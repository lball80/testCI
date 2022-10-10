using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace testWorker.Models
{
    public class Model
    {

        public DateTime GetTestSp()
        {
            string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";

            NpgsqlConnection conn = new NpgsqlConnection($"Host={dbHost};Database=test_db;Username=postgres;Password=postgres");

            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sch.test_func";

            comm.Parameters.Add(
                new NpgsqlParameter("p_out", DbType.DateTime) { Direction = ParameterDirection.Output }
            );
            conn.Open();
            comm.ExecuteNonQuery();

            var ret = (DateTime)comm.Parameters[0].Value;

            conn.Close();
            return ret;
        }
    }
}