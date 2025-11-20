using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EZSocketNc.Mqtts.Dtos
{
    public class EquipmentError : BaseMsg
    {
        [JsonProperty("data")]
        public EquipmentErrorData Data { get; set; }
    }

    public class EquipmentErrorData
    {
        public EquipmentErrorData()
        {
        }
        public EquipmentErrorData(string errorInstanceId, string errorCode, string errorMsg) : this()
        {

            ErrorInstanceId = errorInstanceId;
            ErrorCode = errorCode;
            ErrorMsg = errorMsg;
        }
        public EquipmentErrorData(string errorInstanceId, string errorCode,string errorType, string errorMsg) : this()
        {

            ErrorInstanceId = errorInstanceId;
            ErrorCode = errorCode;
            ErrorType = errorType;
            ErrorMsg = errorMsg;
        }
        [JsonProperty("errorInstanceId")]
        public string ErrorInstanceId { get; set; }
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }
        [JsonProperty("errorType")]
        public string ErrorType { get; set; }
        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }
    }
}
