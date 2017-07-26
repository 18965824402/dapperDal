/****************************************************** 

    文件名称：IDistributedTransaction.cs
    功能描述：事务
    作    者：Jxw
    日    期：2016.04.19
    修改记录：

*******************************************************/

using System;
using System.Transactions;

namespace Base.Dal
{
    public interface IDistributedTransaction : IDisposable
    {
        void Complete();
    }

    public class DistributedTransaction : IDistributedTransaction
    {
        private readonly IUnitWork[] _unitWorks;
        private readonly TransactionScope _transactionScope;
        public DistributedTransaction(params IUnitWork[] unitWork)
        {
            _unitWorks = unitWork;
            _transactionScope = new TransactionScope();
        }

        public void Complete()
        {
            //foreach (var unitWork in _unitWorks)
            //{
            //    unitWork.Commit();
            //}
            _transactionScope.Complete();
        }
        public void Dispose()
        {
            _transactionScope.Dispose();
        }
    }

}
