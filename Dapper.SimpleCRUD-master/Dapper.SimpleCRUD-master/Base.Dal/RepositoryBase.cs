using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Base.Dal
{
    public class RepositoryBase<TEntity, TKey> where TEntity : Entity, new()
    {
        private IDbConnection conn;
        private IDbTransaction transaction;

        public RepositoryBase(IDbConnection _conn, IDbTransaction _transaction)
        {
            conn = _conn;
            transaction = _transaction;

        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>By default filters on the Id column</para>
        /// <para>-Id column name can be overridden by adding an attribute on your primary key property [Key]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a single entity by a single id from table T</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a single entity by a single id from table T.</returns>
        public virtual T Get<T>(object id)
        {
            return conn.Get<T>( id, transaction);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="whereConditions"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a list of entities with optional exact match where conditions</returns>
        public virtual IEnumerable<T> GetList<T>(object whereConditions)
        {
            return conn.GetList<T>(whereConditions, transaction);
        }

        ///// <summary>
        ///// <para>By default queries the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>conditions is an SQL where clause and/or order by clause ex: "where name='bob'" or "where age>=@Age"</para>
        ///// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>Returns a list of entities that match where conditions</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="conditions"></param>
        ///// <param name="parameters"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>Gets a list of entities with optional SQL where conditions</returns>
        public virtual IEnumerable<T> GetList<T>(string conditions, object parameters = null)
        {
            return conn.GetList<T>(conditions, parameters, transaction);
        }

        /////// <summary>
        /////// <para>By default queries the table matching the class name</para>
        /////// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /////// <para>Returns a list of all entities</para>
        /////// </summary>
        /////// <typeparam name="T"></typeparam>
        /////// <param name="connection"></param>
        /////// <returns>Gets a list of all entities</returns>
        ////public virtual IEnumerable<T> GetList<T>(this IDbConnection connection)
        ////{
        ////    return connection.GetList<T>(new { });
        ////}

        ///// <summary>
        ///// <para>By default queries the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age" - not required </para>
        ///// <para>orderby is a column or list of columns to order by ex: "lastname, age desc" - not required - default is by primary key</para>
        ///// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>Returns a list of entities that match where conditions</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="pageNumber"></param>
        ///// <param name="rowsPerPage"></param>
        ///// <param name="conditions"></param>
        ///// <param name="orderby"></param>
        ///// <param name="parameters"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>Gets a paged list of entities with optional exact match where conditions</returns>
        public virtual IEnumerable<T> GetListPaged<T>( int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {

            return conn.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, parameters, transaction);
        }

        ///// <summary>
        ///// <para>Inserts a row into the database</para>
        ///// <para>By default inserts into the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Insert filters out Id column and any columns with the [Key] attribute</para>
        ///// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the int? type, otherwise null</para>
        ///// </summary>
        ///// <param name="connection"></param>
        ///// <param name="entityToInsert"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The ID (primary key) of the newly inserted record if it is identity using the int? type, otherwise null</returns>
        public virtual int? Insert<TEntity>(TEntity entityToInsert)
        {
            return conn.Insert<TEntity>(entityToInsert, transaction);
        }

        ///// <summary>
        ///// <para>Inserts a row into the database, using ONLY the properties defined by TEntity</para>
        ///// <para>By default inserts into the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Insert filters out Id column and any columns with the [Key] attribute</para>
        ///// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</para>
        ///// </summary>
        ///// <param name="connection"></param>
        ///// <param name="entityToInsert"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</returns>
        public virtual TKey Insert<TKey, TEntity>(TEntity entityToInsert)
        {
            return conn.Insert<TKey, TEntity>(entityToInsert, transaction);
        }

        ///// <summary>
        ///// <para>Updates a record or records in the database with only the properties of TEntity</para>
        ///// <para>By default updates records in the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Updates records where the Id property and properties with the [Key] attribute match those in the database.</para>
        ///// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>Returns number of rows effected</para>
        ///// </summary>
        ///// <param name="connection"></param>
        ///// <param name="entityToUpdate"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The number of effected records</returns>
        public virtual int Update<TEntity>(TEntity entityToUpdate)
        {
            return conn.Update<TEntity>(entityToUpdate, transaction);
        }

        ///// <summary>
        ///// <para>Deletes a record or records in the database that match the object passed in</para>
        ///// <para>-By default deletes records in the table matching the class name</para>
        ///// <para>Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>Returns the number of records effected</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="entityToDelete"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The number of records effected</returns>
        public virtual int Delete<T>(T entityToDelete)
        {

            return conn.Delete<T>(entityToDelete, transaction);
        }

        ///// <summary>
        ///// <para>Deletes a record or records in the database by ID</para>
        ///// <para>By default deletes records in the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Deletes records where the Id property and properties with the [Key] attribute match those in the database</para>
        ///// <para>The number of records effected</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="id"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The number of records effected</returns>
        public virtual int Delete<T>(object id)
        {

            return conn.Delete<T>(id, transaction);
        }

        ///// <summary>
        ///// <para>Deletes a list of records in the database</para>
        ///// <para>By default deletes records in the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Deletes records where that match the where clause</para>
        ///// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        ///// <para>The number of records effected</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="whereConditions"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The number of records effected</returns>
        public virtual int DeleteList<T>(object whereConditions)
        {


            return conn.DeleteList<T>(whereConditions, transaction);
        }

        ///// <summary>
        ///// <para>Deletes a list of records in the database</para>
        ///// <para>By default deletes records in the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Deletes records where that match the where clause</para>
        ///// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"</para>
        ///// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="conditions"></param>
        ///// <param name="parameters"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>The number of records effected</returns>
        public virtual int DeleteList<T>(string conditions, object parameters = null)
        {

            return conn.DeleteList<T>(conditions, parameters, transaction);
        }

        ///// <summary>
        ///// <para>By default queries the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Returns a number of records entity by a single id from table T</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age" - not required </para>
        ///// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="conditions"></param>
        ///// <param name="parameters"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>Returns a count of records.</returns>
        public virtual int RecordCount<T>(string conditions = "", object parameters = null)
        {

            return conn.RecordCount<T>(conditions, parameters, transaction);
        }

        ///// <summary>
        ///// <para>By default queries the table matching the class name</para>
        ///// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        ///// <para>Returns a number of records entity by a single id from table T</para>
        ///// <para>Supports transaction and command timeout</para>
        ///// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="connection"></param>
        ///// <param name="whereConditions"></param>
        ///// <param name="transaction"></param>
        ///// <param name="commandTimeout"></param>
        ///// <returns>Returns a count of records.</returns>
        public virtual int RecordCount<T>(object whereConditions)
        {
            return conn.RecordCount<T>(whereConditions, transaction);
        }





    }


}
