using System.Collections.Generic;

namespace EZSocketNc.Db
{
    public interface ICmdStorage
    {
        List<CmdRetryEntity> QueryAll(string id);
        List<CmdRetryEntity> All();

        bool Insert(CmdRetryEntity entity);

        bool Remove(string id);

        bool Update(CmdRetryEntity entity);
    }
}
