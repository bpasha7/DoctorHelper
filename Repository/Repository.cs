using Entities;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository
    {

    }


    public class MyLocalRepository : IRepository, IDisposable
    {
        private OleDbConnection _connection;

        public MyLocalRepository()
        {
            _connection = new OleDbConnection(Properties.Resource.ConnectionString);
        }
        public int SavePatient(Patient P)
        {
            try
            {
                _connection?.Open();
                OleDbCommand myAccessCommand = new OleDbCommand($"insert into Patients (Name, BDate, Gender, LName) values ('{P.Name}', '{P.BDate}', '{P.Gender}', '{P.Lname}');", _connection);
                myAccessCommand.ExecuteNonQuery();
                myAccessCommand.CommandText = "Select @@Identity";
                return (int)myAccessCommand.ExecuteScalar();
            }
            catch (OleDbException ex)
            {
                if (ex.ErrorCode == -2147467259)
                    return -1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public int SaveDoc(Docs Doc)
        {
            try
            {
                int count = 0;
                _connection?.Open();
                try
                {
                    OleDbCommand myAccessCommand = new OleDbCommand($"insert into Docs (Name, [Date], exId, PId) values ('{Doc.Name}', '{Doc.Date}', {Doc.exId}, {Doc.Pid});", _connection);
                    myAccessCommand.ExecuteNonQuery();
                    count++;
                }
                catch (Exception ex)
                {

                }
                return count;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public int UpdateNodes(List<TreeNode> Nodes)
        {
            try
            {
                int count = 0;
                _connection?.Open();
                foreach (var node in Nodes)
                {
                    try
                    {
                        OleDbCommand myAccessCommand = new OleDbCommand($"update Trees set [Name] = '{node.Name}' where Id = {node.Id};", _connection);
                        myAccessCommand.ExecuteNonQuery();
                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public int SaveNodes(List<TreeNode> Nodes)
        {
            try
            {
                int count = 0;
                _connection?.Open();
                foreach (var node in Nodes)
                {
                    try
                    {
                        OleDbCommand myAccessCommand = new OleDbCommand($"insert into Trees (epId, Name, pId) values ({node.epId}, '{node.Name}', {node.pId});", _connection);
                        myAccessCommand.ExecuteNonQuery();
                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public int DeleteNodes(List<TreeNode> Nodes)
        {
            try
            {
                int count = 0;
                _connection?.Open();
                foreach (var node in Nodes)
                {
                    try
                    {
                        OleDbCommand myAccessCommand = new OleDbCommand($"delete from Trees where epId = {node.epId} and Id = {node.Id};", _connection);
                        myAccessCommand.ExecuteNonQuery();
                        count++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public int GetExaminationId(string Name)
        {
            try
            {
                _connection?.Open();
                OleDbCommand myAccessCommand = new OleDbCommand($"select Id from Examination where Name = '{Name}';", _connection);
                var reader = myAccessCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return Convert.ToInt32(reader["Id"]);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public Dictionary<string, int> GetAllExamitations()
        {
            try
            {
                _connection?.Open();
                OleDbCommand myAccessCommand = new OleDbCommand($"select * from Examinations;", _connection);
                var reader = myAccessCommand.ExecuteReader();
                var examinations = new Dictionary<string, int>();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var ex = new Examination(reader);
                        examinations.Add(ex.Short, ex.Id);
                    }
                return examinations;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public IEnumerable<Patient> GetPatients(string LastName)
        {
            try
            {
                _connection?.Open();
                OleDbCommand myAccessCommand = new OleDbCommand($"select * from Patients where LName like '%{LastName}%';", _connection);
                var reader = myAccessCommand.ExecuteReader();
                var patients = new List<Patient>();
                if (reader.HasRows)
                    while (reader.Read())
                        patients.Add(new Patient(reader));
                return patients;
            }
            catch (Exception ex)
            {
                string lines = ex.ToString();

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter($@"{Directory.GetCurrentDirectory()}\err.txt");
                file.WriteLine(lines);

                file.Close();
                return null;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public Dictionary<int, string> GetDevices()
        {
            try
            {
                _connection?.Open();
                OleDbCommand myAccessCommand = new OleDbCommand($"select * from Devices;", _connection);
                var reader = myAccessCommand.ExecuteReader();
                var dic = new Dictionary<int, string>();
                if (reader.HasRows)
                    while (reader.Read())
                        dic.Add(reader.GetInt32(0), reader.GetString(1));
                return dic;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public IEnumerable<TreeNode> GetAllTreeNodes(int exId, string Name)
        {
            try
            {
                _connection?.Open();
                OleDbCommand myAccessCommand = new OleDbCommand($"select Trees.Id, Trees.Name, Trees.pId, Trees.epId from ExProp, Trees where ExProp.Id = Trees.epId and ExProp.Name = '{Name}' and ExProp.eId = {exId};", _connection);
                var reader = myAccessCommand.ExecuteReader();
                var treeNodes = new List<TreeNode>();
                if (reader.HasRows)
                    while (reader.Read())
                        treeNodes.Add(new TreeNode(reader));
                return treeNodes;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
    //public class MyRepository : IRepository, IDisposable
    //{
    //    private OleDbConnection _connection;
    //    private object _lock;

    //    public MyRepository()
    //    {
    //        _connection = new OleDbConnection(Properties.Resource.ConnectionString);
    //    }
    //    /// <summary>
    //    /// Создание обследования
    //    /// </summary>
    //    /// <param name="Name">Название обследования</param>
    //    /// <returns></returns>
    //    public async Task<bool> CreateExaminationAsync(string Name)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"insert into Examinations (Name) values ('{Name}');", _connection);
    //            await myAccessCommand.ExecuteNonQueryAsync();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    /// <summary>
    //    /// Создание поля для обследования
    //    /// </summary>
    //    /// <returns></returns>
    //    public async Task<bool> CreateExaminationPropertyAsync(int eId, string Name)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"insert into ExProp (eId, Name) values ({eId}, '{Name}');", _connection);
    //            await myAccessCommand.ExecuteNonQueryAsync();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<bool> CreateTempalteAsync(int epId, string Name)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"insert into Templates (epId, Name) values ({epId}, '{Name}');", _connection);
    //            await myAccessCommand.ExecuteNonQueryAsync();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<int> GetExaminationIdAsync(string Name)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select Id from Examination where Name = '{Name}';", _connection);
    //            var reader = await myAccessCommand.ExecuteReaderAsync();
    //            if(reader.HasRows)
    //            {
    //                reader.Read();
    //                return Convert.ToInt32(reader["Id"]);
    //            }
    //            return -1;
    //        }
    //        catch (Exception ex)
    //        {
    //            return -1;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<bool> UpdateTempalteAsync(int Id, string Name, string Value)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"update Templates set [Name] = '{Name}', [Value] = '{Value}' where Id = {Id};", _connection);
    //            await myAccessCommand.ExecuteNonQueryAsync();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<IEnumerable<Examination>> GetAllExamitationsAsync()
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select * from Examinations;", _connection);
    //            var reader = await myAccessCommand.ExecuteReaderAsync();
    //            var examinations = new List<Examination>();
    //            if (reader.HasRows)        
    //                while (reader.Read())
    //                    examinations.Add( new Examination(reader));
    //            return examinations;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<IEnumerable<ExProperty>> GetAllExamitationsPropertiesByIdAsync(int Id)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select * from ExProp where eId ={Id};", _connection);
    //            var reader = await myAccessCommand.ExecuteReaderAsync();
    //            var exProp = new List<ExProperty>();
    //            if (reader.HasRows)
    //                while (reader.Read())
    //                    exProp.Add(new ExProperty(reader));
    //            return exProp;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<IEnumerable<Template>> GetAllTemplatesAsync(string exName, string Name)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select Templates.Id, Templates.Name, Templates.Value from Examinations, ExProp, Templates where Examinations.Id =ExProp.eId and ExProp.Id = Templates.epId and ExProp.Name = '{Name}' and Examinations.Short = '{exName}';", _connection);
    //            var reader = await myAccessCommand.ExecuteReaderAsync();
    //            var templates = new List<Template>();
    //            if (reader.HasRows)
    //                while (reader.Read())
    //                    templates.Add(new Template(reader));
    //            return templates;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public Dictionary<int, string> GetDevices()
    //    {
    //        try
    //        {
    //            _connection?.Open();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select * from Devices;", _connection);
    //            var reader = myAccessCommand.ExecuteReader();
    //            var dic = new Dictionary<int, string>();
    //            if (reader.HasRows)
    //                while (reader.Read())
    //                    dic.Add(reader.GetInt32(0), reader.GetString(1));
    //            return dic;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<IEnumerable<TreeNode>> GetAllTreeNodesAsync(string exName, string Name)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select Trees.Id, Trees.Name, Trees.pId from Examinations, ExProp, Trees where Examinations.Id =ExProp.eId and ExProp.Id = Trees.epId and ExProp.Name = '{Name}' and Examinations.Short = '{exName}';", _connection);
    //            var reader = await myAccessCommand.ExecuteReaderAsync();
    //            var treeNodes = new List<TreeNode>();
    //            if (reader.HasRows)
    //                while (reader.Read())
    //                    treeNodes.Add(new TreeNode(reader));
    //            return treeNodes;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public async Task<IEnumerable<Template>> GetAllTemplatesAsync(int Id)
    //    {
    //        try
    //        {
    //            await _connection?.OpenAsync();
    //            OleDbCommand myAccessCommand = new OleDbCommand($"select * from Templates where epId ={Id};", _connection);
    //            var reader = await myAccessCommand.ExecuteReaderAsync();
    //            var templates = new List<Template>();
    //            if (reader.HasRows)
    //                while (reader.Read())
    //                    templates.Add(new Template(reader));
    //            return templates;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            _connection?.Close();
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        _connection?.Close();
    //    }
    //}
}
