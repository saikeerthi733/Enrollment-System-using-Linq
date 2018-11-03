/*****************************************************
 * Assignment: 3                                     *
 * Due Date: Thursday, 11th Oct                      *
 *                                                   *
 *Name: Sai Keerthi Tsundupalli (Z1836733)           *
 * Partner Name: Komal Thakkar (Z1834925)            *
 *                                                   *
 ****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tsundupalli_Assign3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ReadStudentInputFile();
            ReadCourseInputFile();
            ReadMajorInputFile();
            ReadGradeInputFile();
            Application.Run(new Form1());
        }
        /******** Reading input files ********/
        #region
        public static void ReadStudentInputFile()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\2188_a3_input01.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokens = line.Split(',');
                    Student s = new Student(Convert.ToUInt32(tokens[0]), tokens[1], tokens[2], tokens[3], float.Parse(tokens[5]), (AcademicYear)Enum.Parse(typeof(AcademicYear), tokens[4]));
                    Student.StudentPool.Add(s);
                    line = sr.ReadLine();
                }
                Student.StudentPool.Sort();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                //Console.ReadKey();
            }
        }
        public static void ReadCourseInputFile()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\2188_a3_input02.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokens = line.Split(',');

                    Course c = new Course();
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        c.DeptCode = tokens[0];
                        c.CourseNumber = Convert.ToUInt16(tokens[1]);
                        c.SectionNumber = tokens[2];
                        c.CreditHours = Convert.ToUInt16(tokens[3]);
                        c.MaxCapacity = Convert.ToUInt16((tokens[4]));

                    }
                    Course.CoursePool.Add(c);
                    line = sr.ReadLine();
                }
                Course.CoursePool.Sort();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public static void ReadMajorInputFile()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\2188_a3_input03.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Course.Major.Add(line);
                    line = sr.ReadLine();
                }
                Course.CoursePool.Sort();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        #endregion
        public static void ReadGradeInputFile()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("..\\..\\2188_a3_input04.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    string[] tokens = line.Split(',');

                    Grades grade = new Grades(Convert.ToUInt32(tokens[0]), tokens[1], Convert.ToUInt32(tokens[2]), tokens[3]);

                    Grades.GradesPool.Add(grade);
                    line = sr.ReadLine();
                }
                Grades.GradesPool.Sort();
                Grades.GradingList.Add("A+", 1);
                Grades.GradingList.Add("A", 2);
                Grades.GradingList.Add("A-", 3);
                Grades.GradingList.Add("B+", 4);
                Grades.GradingList.Add("B", 5);
                Grades.GradingList.Add("B-", 6);
                Grades.GradingList.Add("C++", 7);
                Grades.GradingList.Add("C", 8);
                Grades.GradingList.Add("C-", 9);
                Grades.GradingList.Add("D", 10);
                Grades.GradingList.Add("F", 11);
                                            
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

    }
}

    

