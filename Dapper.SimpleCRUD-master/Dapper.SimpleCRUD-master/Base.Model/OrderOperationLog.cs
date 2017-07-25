using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model
{
    [Table("OrderOperationLog")]
    public partial class OrderOperationLog : Entity
    {
        [Key]
        public int Id
        {
            set;
            get;
        }

        /// <summary>
        /// 操作时间
        /// </summary>

        public DateTime? CreateTime
        {
            set;
            get;
        }


        /// <summary>
        /// 操作者
        /// </summary>

        public virtual string Operator { get; set; }

        /// <summary>
        /// 操作者id
        /// </summary>

        public virtual int OperatorId { get; set; }

        /// <summary>
        /// ip 地址
        /// </summary>

        public virtual string IP { get; set; }


        /// <summary>
        /// 操作标题
        /// </summary>

        public string Title
        {
            set;
            get;
        }
        /// <summary>
        /// 操作内容
        /// </summary>

        public string Message
        {
            set;
            get;
        }
        /// <summary>
        /// 总订单号
        /// </summary>

        public string BillNo
        {
            set;
            get;
        }


    }
}
