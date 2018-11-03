/*****************************************************
 * Assignment: 3                                     *
 * Due Date: Thursday, 11th Oct                      *
 *                                                   *
 * Name: Sai Keerthi Tsundupalli (Z1836733)          *
 * Partner Name: Komal Thakkar (Z1834925)            *
 *                                                   *
 ****************************************************/

using System;
using System.Collections.Generic;

namespace Tsundupalli_Assign3
{
    enum AcademicYear { Freshman, Sophomore, Junior, Senior, PostBacc };
    class Student : IComparable
    {
        /******** Class Attributes and Getter Setter Methods ********/
        #region
        private readonly uint zID;
        private string firstName;
        private string lastName;
        private string major;
        private Nullable<float> cumulativeGPA;
        private Nullable<ushort> numOfCreditHoursEnrolled;
        private AcademicYear academicYear;
        public static List<Student> StudentPool = new List<Student>();

        public uint ZID
        {
            get { return zID; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { if (value != null) firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { if (value != null) lastName = value; }
        }
        public string Major
        {
            get { return major; }
            set { if (value != null) major = value; }
        }
        public Nullable<float> CumulativeGPA
        {
            get { return cumulativeGPA; }
            set
            {
                if (value - 4.000 <= 0)
                {
                    cumulativeGPA = value;
                }
                else
                {
                    //throw new Exception("GPA must be between 0 to 4.");
                }
            }
        }
        public Nullable<ushort> NumOfCreditHoursEnrolled
        {
            get { return numOfCreditHoursEnrolled; }
            set
            {
                if (value >= 0 && value <= 18) numOfCreditHoursEnrolled = value;
            }
        }
        public AcademicYear AcademicYears
        {
            get { return academicYear; }
            set { academicYear = value; }
        }
        #endregion

        /******** Constructors ********/
        #region
        public Student()
        {
            FirstName = null;
            LastName = null;
            Major = null;
            CumulativeGPA = null;
            NumOfCreditHoursEnrolled = 0;
            AcademicYears = 0;
        }

        public Student(uint zid, string lName, string fName, string majorStr, float cumGPA, AcademicYear ay)
        {
            if (zid > 1000000)
            {
                zID = zid;
                FirstName = fName;
                LastName = lName;
                Major = majorStr;
                CumulativeGPA = cumGPA;
                AcademicYears = ay;
                NumOfCreditHoursEnrolled = 0;
            }
            else
            {
                throw new Exception("Invalid Student Z-ID.");
            }
        }
        #endregion

        /******** Built in Methods ********/
        #region
        public int CompareTo(Object alpha)
        {
            if (alpha == null) return 1;
            Student s = alpha as Student;
            if (s != null)
            {
                return this.zID.CompareTo(s.zID);
            }
            else
            {
                throw new ArgumentException("[Student]:CompareTo argument is not a Student");
            }
        }

        public override string ToString()
        {
            return String.Format("{0,-8} -- {1,13}, {2,-10} [{3,9}] ({4,15}) | {5:0.000} |", "z" + ZID, LastName, FirstName, AcademicYears, Major, CumulativeGPA);
        }
        #endregion
    }
}
