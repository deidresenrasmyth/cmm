using CollegeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CollegeManagementSystem.Controllers
{
    public class CollegeController : Controller
    {
        // GET: College
        public ActionResult Index()
        {
            List<Student> students = GetStudents();

            return View("Students", students);
        }

        // GET: College/Details/5
        public ActionResult Details(string id)
        {
            Student student = GetStudent(id);
            return View(student);
        }

        // GET: College/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: College/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if(string.IsNullOrEmpty(student.Country) || string.IsNullOrEmpty(student.EmailAddr)  || string.IsNullOrEmpty(student.LastName)|| string.IsNullOrEmpty(student.FirstName))
            {
                student.ErrorMsg = "Please review the information and try again";
                return View("Create", student);
            }


            try
            {

                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["CollegeManagementSystem"].ConnectionString;
                string query = @"INSERT INTO [dbo].[Students]
                               ([FirstName]
                               ,[LastName]
                               ,[BirthDate]
                               ,[EmailAddr]
                               ,[Country])
                                    VALUES
                               (@firstName,@lastName,@birthDate,@emailAddr,@country)";
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = cn;
                myCommand.CommandText = query;
                myCommand.Parameters.AddWithValue("@firstName", student.FirstName);
                myCommand.Parameters.AddWithValue("@lastName", student.LastName);
                myCommand.Parameters.AddWithValue("@birthDate", student.BirthDate);
                myCommand.Parameters.AddWithValue("@emailAddr", student.EmailAddr);
                myCommand.Parameters.AddWithValue("@country", student.Country);

                cn.Open();
                var id = myCommand.ExecuteNonQuery();
                cn.Close();
                return View("Details", student);
            }
            catch (Exception ex)
            {
                //Do something to handle the exception
                return View("Details", student);
            }

        }

        // GET: College/Edit/5
        public ActionResult Edit(string id)
        {
            Student student = GetStudent(id);
            return View(student);
        }

        // POST: College/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Student student)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["CollegeManagementSystem"].ConnectionString;
                string query = @"UPDATE [dbo].[Students]
                                   SET [FirstName] = @firstName
                                      ,[LastName] = @lastName
                                      ,[BirthDate] = @birthDate
                                      ,[EmailAddr] =@emailAddr
                                      ,[Country] = @country
                                 WHERE StudentId = @studentId";
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = cn;
                myCommand.CommandText = query;
                myCommand.Parameters.AddWithValue("@firstName", student.FirstName);
                myCommand.Parameters.AddWithValue("@lastName", student.LastName);
                myCommand.Parameters.AddWithValue("@birthDate", student.BirthDate);
                myCommand.Parameters.AddWithValue("@emailAddr", student.EmailAddr);
                myCommand.Parameters.AddWithValue("@country", student.Country);
                myCommand.Parameters.AddWithValue("@studentId", id);

                cn.Open();
                myCommand.ExecuteNonQuery();
                cn.Close();
                return View("Details", student);
            }
            catch (Exception ex)
            {
                //Do something to handle the exception
                return View("Details", student);
            }

        }

        // GET: College/Delete/5
        public ActionResult Delete(string id)
        {
            Student student = GetStudent(id);
            return View(student);
        }

        // POST: College/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["CollegeManagementSystem"].ConnectionString;
                string query = @"
                                DELETE FROM [dbo].[Students]
                                    where  StudentId = @studentId";
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = cn;
                myCommand.CommandText = query;
                myCommand.Parameters.AddWithValue("@studentId", id);
                cn.Open();
                myCommand.ExecuteNonQuery();
                cn.Close();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //Do something to handle the exception
                return RedirectToAction("Index");
            }
        
        }


        private List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CollegeManagementSystem"].ConnectionString;
            try
            {
                myConnection.Open();
                string query = @"
                                SELECT [StudentId]
                                      ,[FirstName]
                                      ,[LastName]
                                      ,[BirthDate]
                                      ,[EmailAddr]
                                      ,[Country]
                                  FROM [CollegeDavid].[dbo].[Students]";


                SqlCommand myCommand = new SqlCommand
                {
                    Connection = myConnection,
                    CommandText = query
                };


                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Student student = new Student()
                    {
                        BirthDate = Convert.ToDateTime(myReader["BirthDate"]),
                        Country = myReader["Country"].ToString(),
                        EmailAddr = myReader["EmailAddr"].ToString(),
                        FirstName = myReader["FirstName"].ToString(),
                        LastName = myReader["LastName"].ToString(),
                        StudentId = myReader["StudentId"].ToString()

                    };
                    students.Add(student);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                myConnection.Close();
            }

            return students;
        }

        private Student GetStudent(string id)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CollegeManagementSystem"].ConnectionString;
            try
            {
                myConnection.Open();
                string query = @"
                                SELECT [StudentId]
                                      ,[FirstName]
                                      ,[LastName]
                                      ,[BirthDate]
                                      ,[EmailAddr]
                                      ,[Country]
                                  FROM [CollegeDavid].[dbo].[Students]
                                       Where StudentId=@studentId";


                SqlCommand myCommand = new SqlCommand
                {
                    Connection = myConnection,
                    CommandText = query,

                };
                myCommand.Parameters.AddWithValue("@studentId", id);

                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Student student = new Student()
                    {
                        BirthDate = Convert.ToDateTime(myReader["BirthDate"]),
                        Country = myReader["Country"].ToString(),
                        EmailAddr = myReader["EmailAddr"].ToString(),
                        FirstName = myReader["FirstName"].ToString(),
                        LastName = myReader["LastName"].ToString(),
                        StudentId = myReader["StudentId"].ToString()

                    };
                    return student;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                myConnection.Close();
            }

            return null;
        }
    }
}
