
using System;
using System.Collections.Generic;
using System.Linq;

namespace EZSocketNc.Db
{
    public class CmdStorage : ICmdStorage
    {  
        private StorageDbContext _dbContext = StorageDbContext.Instance;

        private static object _lockObject = new object();

        public bool Insert(CmdRetryEntity entity)
        {
            bool result = false;
            try
            {
                lock (_lockObject)
                {
                    entity.CreateTime = DateTime.Now;
                    this._dbContext.CmdStorages.Add(entity);
                    this._dbContext.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error(ex);
                result = false;
            }
            return result;
        }

        public List<CmdRetryEntity> QueryAll(string id)
        {
            List<CmdRetryEntity> result = new List<CmdRetryEntity>();
            try
            {
                lock (_lockObject)
                {
                    result = (from q in this._dbContext.CmdStorages
                              where q.Id == id
                              select q).ToList();
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error(ex);
                result = new List<CmdRetryEntity>();
            }
            return result;
        }
        public List<CmdRetryEntity> All()
        {
            List<CmdRetryEntity> result = new List<CmdRetryEntity>();
            try
            {
                lock (_lockObject)
                {
                    result = this._dbContext.CmdStorages.ToList();
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error(ex);
                result = new List<CmdRetryEntity>();
            }
            return result;
        }

        public bool Remove(string id)
        {
            bool result;
            try
            {
                lock (_lockObject)
                {
                    IQueryable<CmdRetryEntity> queryable = from q in this._dbContext.CmdStorages
                                                     where q.Id.Equals(id)
                                                     select q;
                    if (queryable.Count() > 0)
                    {
                        foreach (CmdRetryEntity entity in queryable)
                        {
                            this._dbContext.CmdStorages.Remove(entity);
                        }
                        this._dbContext.SaveChanges();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Update(CmdRetryEntity entity)
        {
            bool result;
            try
            {
                lock (_lockObject)
                {
                    var data = this._dbContext.CmdStorages.FirstOrDefault( q => q.Id.Equals(entity.Id));
                    if (data!=null)
                    {
                        data.DataJson = entity.DataJson;
                        data.RetryTimes = entity.RetryTimes;
                        data.CreateTime = DateTime.Now;
                        this._dbContext.SaveChanges();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Utils.LogHelper.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
