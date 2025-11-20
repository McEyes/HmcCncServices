using AoiAdapter.Handlers;

using EZSocketNc.Commons;
using EZSocketNc.Configs;
using EZSocketNc.Extensions;

using HmcCncServices.Configs;

using JAgentServiceProtocol;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;
using HmcCncServices;
using System.Windows.Forms;

namespace AoiAdapter.Services
{
    public delegate void RunWinformHandler<T>(T data);
    [JAgentComponent(Name = "HmcCncAdapterService", Author = "liyh", Description = "Hmc Cnc设备数据采集服务")]
    public class HmcCncAdapterService : IJAgentService
    {
        private readonly RunWinformHandler<System.Windows.Forms.Form> showMonitorHandler;
        private readonly string CurrNTID = Environment.UserName;
        private readonly string MachineName = Environment.MachineName;
        public string Name { get; set; } = "HmcCncAdapterService";
        private CancellationTokenSource _cts;

        public bool IsRunning { get; set; } = false;

        public static Iloger Log;
        private IEtcdConfiger _etcd;
        private List<CncServiceConfig> _adapterConfig;
        private List<HmcCncHandler> _adapterList = new List<HmcCncHandler>();
        [JServiceCreateFunction("Create")]
        public static IJAgentService Create()
        {
            return new HmcCncAdapterService();
        }
        private HmcCncAdapterService()
        {
            showMonitorHandler = new RunWinformHandler<System.Windows.Forms.Form>(ShowWindow);
        }


        public void Open(JAgentContext context)
        {
            Generics.Context = context;
            Log = context.Loger;
            string key = $"/jagent/deviceadapter/{Generics.HostName}";
            Log.Debug($"[{Generics.HostName}][Etcd]{key}");
            _etcd = context.EtcdManager.GetEtcdConfiger(key);
            _etcd.OnValueChange += _etcd_OnValueChange;
        }

        public void Start()
        {
            Log.Debug($"start {Name}");
            IsRunning = true;
            ResolveAdapterConfig(_etcd.GetConfig());
            SyncStarForm();
        }

        public void Stop()
        {
            Log.Debug($"stop {Name}");
            _adapterList.ForEach(q =>
            {
                Log.Info($"[{Name}] stop");
                q.Stop();
            });
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();
                try
                {
                    if (mainForm != null && !mainForm.IsDisposed)
                    {
                        mainForm.CloseWin();
                        mainForm = null;
                    }
                }
                catch
                {
                    Log.Debug($"[{MachineName}]-[{CurrNTID}]main Form closed");
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"[{MachineName}]-[{CurrNTID}]main Form close error:{ex.Message},{ex.StackTrace}");
            }
            IsRunning = false;
        }

        private void _etcd_OnValueChange(string value)
        {
            ResolveAdapterConfig(value);
        }

        private void ResolveAdapterConfig(string value)
        {
            try
            {
                Log.Info($"[{Generics.HostName}][Etcd]当前适配器配置更新{value}");
                var result = value.FromJSON<List<CncServiceConfig>>();// JsonExtension.FromJSON<List<AoiConfig>>(value);
                if (result != null)
                {
                    Log.Info($"[{Generics.HostName}][Etcd]成功解析Adapter配置：{result.Count()}条");
                    _adapterConfig = result.Where(f => f.Enable == true || "HmcCnc".Equals(f.Kind, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    UpdateAdapter();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{Generics.HostName}][Etcd]解析Adapter配置异常：{ex.Message},\r\n{ex.StackTrace}");
            }
        }

        private void UpdateAdapter()
        {
            if (IsRunning)
            {
                Log.Info($"[{Name}] HmcCncAdapterService UpdateAdapter");
                _adapterList.ForEach(q =>
                {
                    Log.Info($"[{Name}] HmcCncAdapterService UpdateAdapter item");
                    q.Stop();
                });
                _adapterList.Clear();
                if (_adapterConfig != null)
                {
                    _adapterConfig = _adapterConfig.Where(f => f.Enable == true && "HmcCnc".Equals(f.Kind, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    _adapterConfig.ForEach(item =>
                    {
                        //if (item.Enable == false || !"HmcCnc".Equals(item.Kind, StringComparison.InvariantCultureIgnoreCase)) return;
                        Log.Info($"[{Generics.HostName}][HmcCnc]启用适配器服务：{item.Name}");
                        HmcCncHandler lm = new HmcCncHandler(item);
                        _adapterList.Add(lm);
                        lm.Start();
                    });
                }
            }
        }



        private MainCnc mainForm;
        private void SyncStarForm()
        {
            _cts = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                while (!_cts.IsCancellationRequested)
                {
                    try
                    {
                        if (mainForm == null || mainForm.IsDisposed)
                        {
                            StartForm();
                        }
                        Application.DoEvents();
                        Thread.Sleep(100);//2 second one time to update the screen
                    }
                    catch (Exception ex)
                    {
                        Generics.Context.Loger.Debug($"[{MachineName}]-[{CurrNTID}]monitor server start error：{ex.Message},{ex.StackTrace}");
                    }
                    finally
                    {
                        if (mainForm == null || mainForm.IsDisposed) _cts.Cancel();
                    }
                }
                if (mainForm != null && !mainForm.IsDisposed)
                {
                    mainForm.CloseWin();
                    mainForm = null;
                }
            });
        }
        private void StartForm()
        {
            try
            {
                mainForm = new MainCnc(Generics.Context, _adapterConfig);
                if (mainForm.InvokeRequired)
                {
                    if (!mainForm.IsHandleCreated) mainForm.Show();
                    mainForm.BeginInvoke(showMonitorHandler, mainForm);
                }
                else
                {
                    mainForm.Show();
                }
            }
            catch (Exception ex)
            {
               Generics.Context.Loger.Error($"[{MachineName}]-[{CurrNTID}]fiald start Cnc winform:{ex.Message},{ex.StackTrace}");
            }
        }
        private void ShowWindow(System.Windows.Forms.Form form)
        {
            try
            {
                if (form != null && !form.IsDisposed)
                {
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                Generics.Context.Loger.Error($"[{MachineName}]-[{CurrNTID}]Cnc winform异常:{ex.Message},{ex.StackTrace}");
            }
        }
    }

}