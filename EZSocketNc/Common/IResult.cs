

using EZSocketNc.EZNc;

namespace EZSocketNc.Commons
{
    public interface IResult
    {
        string SourceId { get; set; }
        bool Success { get; set; }
        int Code { get; set; }
        string Msg { get; set; }
        void SetError(string msg, bool success = false);
        void SetError(string msg, int code);
        void SetError(EZResult errocCde = EZResult.OK, string errorMsg = "");
        void SetError(int code = 0, string errorMsg = "");
        void SetError(EquipmentErrorCode commandExpired, string errorMsg = "");
        void AddError(IResult result);
    }
    public interface IResult<T> : IResult
    {
        T Data { get; set; }
    }
}
