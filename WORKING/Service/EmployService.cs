using Dapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Npgsql;
using Persistance;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System;

using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Service
//Репозиторий
{
    public interface IEmployService
    {
        IEnumerable<Empoy> GetAll();
        bool Add(Empoy model);

        bool Update(Empoy model);
        bool Delete(int id);
        Empoy Getid(int id);


    }
    //Create alternative for Dapper
    public class EmployService : IEmployService
    {
        //for entity
        public readonly GMZDbContext _gmzDbContext;
        public EmployService(GMZDbContext gmzDbContext)
        {
            _gmzDbContext = gmzDbContext;
        }
        /// <summary>
        /// /////////////////////////
        /// </summary>
        /// //for dapper
        string connection = null;
        public EmployService(string conn)
        {
            connection = conn;
        }
        public IEnumerable<Empoy> GetAll()
        {
            using (IDbConnection db = new NpgsqlConnection(connection))
            {
                return db.Query<Empoy>("SELECT * FROM employees").ToList();
            }
        }



        //public IEnumerable<Empoy> GetAll()
        //{

        //    //



        //    //
        //    var result = new List<Empoy>();
        //    try
        //    {
        //        result = _gmzDbContext.employees.ToList();


        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //    return result;

        //}



        //public Empoy Getid(int id)
        //{
        //    var result = new Empoy();
        //    try
        //    {
        //        result = _gmzDbContext.employees.Single(x => x.EmployId == id);

        //        //var originalModel = _gmzDbContext.employees.Single(x => x.EmployId == model.EmployId);

        //        //originalModel.Name = model.Name;
        //        //originalModel.LastName = model.LastName;
        //        //originalModel.Bio = model.Bio;

        //        //_gmzDbContext.Update(originalModel);
        //        //_gmzDbContext.SaveChanges();
        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //    return result;

        //}
        public Empoy Getid(int id)
        {
           
            using (IDbConnection db = new NpgsqlConnection(connection))
            {
                db.Open();
                return db.Query<Empoy>("SELECT * FROM employees WHERE EmployId = @Id;", new { id }).FirstOrDefault();
                
            }
  

        }

        ////
        public bool Add(Empoy model)
        {


            using (IDbConnection db = new NpgsqlConnection(connection))
            {

                db.Open();
                var sqlQuery = "INSERT INTO employees (Name, LastName, Bio) VALUES (@Name, @LastName, @Bio)";
                db.Execute(sqlQuery, model);

            }
            return true;




        }
        //

        //public bool Add(Empoy model)
        //{
        //    try
        //    {
        //        _gmzDbContext.Add(model);
        //        _gmzDbContext.SaveChanges();
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }
        //    return true;

        //}




        //public bool Update(Empoy model)
        //{
        //    try
        //    {

        //        var originalModel = _gmzDbContext.employees.Single(x => x.EmployId == model.EmployId);

        //        originalModel.Name = model.Name;
        //        originalModel.LastName = model.LastName;
        //        originalModel.Bio = model.Bio;

        //        _gmzDbContext.Update(originalModel);
        //        _gmzDbContext.SaveChanges();
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }
        //    return true;

        //}

        public bool Update(Empoy model)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(connection))
                {
                    db.Open();
                    var sqlQuery = "UPDATE employees SET Name = @Name, LastName = @LastName, Bio = @Bio WHERE EmployId = @EmployId;";
                    db.Execute(sqlQuery, model);
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
        //entity
        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        _gmzDbContext.Entry(new Empoy { EmployId = id }).State = EntityState.Deleted;
        //        _gmzDbContext.SaveChanges();
        //    }
        //    catch (System.Exception)
        //    {
        //        return false;
        //    }
        //    return true;

        //}

        //dapper
        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(connection))
                {
                    var sqlQuery = "DELETE FROM employees WHERE EmployId = @Id;";
                    db.Execute(sqlQuery, new { id });
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }




    }
}
