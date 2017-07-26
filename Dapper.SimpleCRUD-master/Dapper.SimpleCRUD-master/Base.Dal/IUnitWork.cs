/****************************************************** 

    文件名称：IUnitWork.cs
    功能描述：事务
    作    者：Jxw
    日    期：2016.04.19
    修改记录：

*******************************************************/

using System;
using System.Threading.Tasks;

namespace Base.Dal
{
    public interface IUnitWork : IDisposable
    {
        //IRepository<T> Get<T>() where T : class, IEntity, new();

        //void Commit();

        //Task<int> CommitAsync();

        //void Rollback();
    }
}
