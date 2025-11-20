using EZSocketNc.Extensions;
using EZSocketNc.EZNc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.Commons
{
    public class Result : IResult
    {
        public Result()
        {

        }
        public Result(string msg, int code = 1001)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                Success = false;
                Code = code;
                Msg = $"{this.GetCallerInfo()} Failed, [0x{Code:x}]{msg}。 ";
            }
        }
        public Result(EZResult errorCode = EZResult.OK)
        {
            if (errorCode != EZResult.OK)
            {
                Success = false;
                Code = errorCode.IntValue();
                Msg = $"{this.GetCallerInfo()}[0x{Code:x}]{errorCode.GetDescription()}。 ";
            }
        }

        public string SourceId { get; set; }
        public bool Success { get; set; } = true;
        public int Code { get; set; } = EZResult.OK.IntValue();
        private string _Msg = "";
        public string Msg
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Msg) && Code != 0) _Msg = $"[0x{Code:x}]{((EZResult)Code).GetDescription()}";
                return _Msg;
            }
            set { _Msg = value; }
        }

        public void SetError(string msg, bool success = false)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                Success = success;
                _Msg += (_Msg?.Length > 0 ? "\r\n" : "") + msg;
            }
        }
        public void SetError(string msg, int code)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                Success = false;
                Code = code;
                _Msg += $"{(_Msg?.Length > 0 ? "\r\n" : "")}[0x{Code:x}]{msg}。 ";
            }
        }

        public virtual void SetError(EZResult errorCode, string errorMsg = "")
        {
            var oldSuccess = Success;
            Success = errorCode == EZResult.OK;
            Code = errorCode.IntValue();
            if (!Success)
            {
                if (oldSuccess)
                    _Msg = $"{this.GetCallerInfo()}[0x{Code:x}]{errorCode.GetDescription()}。 ";
                else
                    _Msg += $"{(_Msg?.Length > 0 ? "\r\n" : "")}{this.GetCallerInfo()}[0x{Code:x}]{errorCode.GetDescription()}。";
            }
            if (!string.IsNullOrWhiteSpace(errorMsg))
                _Msg += $"{(_Msg.Length > 0 ? "\r\n" : "")}{errorMsg}";
        }

        public virtual void SetError(int errorCode, string errorMsg = "")
        {
            var oldSuccess = Success;
            Success = errorCode == (int)EZResult.OK;
            Code = errorCode;
            if (!Success)
            {
                if (oldSuccess)
                    _Msg = $"{this.GetCallerInfo()}[0x{Code:x}]{(errorCode >= 0 ? ((EZResult)errorCode).GetDescription() : "")}。 ";
                else
                    _Msg += $"{(_Msg?.Length > 0 ? "\r\n" : "")}{this.GetCallerInfo()}[0x{Code:x}]{(errorCode >= 0 ? ((EZResult)errorCode).GetDescription() : "")}。";
            }
            if (!string.IsNullOrWhiteSpace(errorMsg))
                _Msg += $"{(_Msg.Length > 0 ? "\r\n" : "")}{errorMsg}";
        }
        


        public virtual void AddError(IResult result)
        {
            if (result == null) return;
            Success = result.Success;
            Code = result.Code;
            if (result.Code != EZResult.OK.IntValue())
            {
                Code = result.Code;
                if (!string.IsNullOrWhiteSpace(result.Msg))
                    _Msg += $"{(_Msg?.Length > 0 ? "\r\n" : "")}{result.Msg}";
            }
        }

        public static Result GetResult(string msg, int code = 1001)
        {
            return new Result(msg, code);
        }
        public virtual void SetError(EquipmentErrorCode errorCode = EquipmentErrorCode.UnknownError, string errorMsg = "")
        {
            Success = false;
            Code = errorCode.IntValue();
            if (errorCode != EquipmentErrorCode.None)
                _Msg += $"[{Code}]{errorCode.GetDescription()}\r\n";
            if (!string.IsNullOrWhiteSpace(errorMsg))
                _Msg += $"{errorMsg}\r\n";
        }

        public static Result GetResult(EZResult errorCode = EZResult.OK)
        {
            return new Result(errorCode);
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public override void AddError(IResult result)
        {
            base.AddError(result);
            if (!result.Success && result is IResult<string>) this.SetError((result as IResult<string>).Data);
            if (result is Result<T>) Data = (result as Result<T>).Data;
        }

    }

}
