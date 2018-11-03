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

namespace Tsundupalli_Assign3
{
    class Course : IComparable
    {
        /******** Class Attributes and Getter Setter Methods ********/
        #region
        private string deptCode;
        private Nullable<uint> courseNumber;
        private string sectionNumber;
        private Nullable<ushort> creditHours;
        private List<int> currentlyEnrolled = new List<int>();
        private Nullable<ushort> numOfCurrentlyEnrolled;
        private Nullable<ushort> maxCapacity;
        public static List<Course> CoursePool = new List<Course>();
        public static List<string> Major = new List<string>();

        public string DeptCode
        {
            get { return deptCode; }
            set
            {
                if (value.Length >= 1 && value.Length <= 4) deptCode = value.ToUpper();
            }
        }
        public Nullable<uint> CourseNumber
        {
            get { return courseNumber; }
            set { if (value >= 100 && value <= 499) courseNumber = value; }
        }
        public string SectionNumber
        {
            get { return sectionNumber; }
            set
            {
                if (value.Length == 4) sectionNumber = value;
            }
        }
        public Nullable<ushort> CreditHours
        {
            get { return creditHours; }
            set { if (value >= 0 && value <= 6) creditHours = value; }
        }
        public Nullable<ushort> MaxCapacity
        {
            get { return maxCapacity; }
            set { maxCapacity = value; }
        }
        public Nullable<ushort> NumOfCurrentlyEnrolled
        {
            get { return numOfCurrentlyEnrolled; }
            set { numOfCurrentlyEnrolled = value; }
        }
        public List<int> CurrentlyEnrolled
        {
            get { return currentlyEnrolled; }
            set { currentlyEnrolled.Add(value[0]); }
        }
        #endregion

        /******** Constructor ********/
        #region
        public Course()
        {
            deptCode = "";
            courseNumber = null;
            sectionNumber = "";
            creditHours = null;
            maxCapacity = 0;
            numOfCurrentlyEnrolled = 0;
        }

        public Course(string deptCode, uint courseNumber, string sectionNumber, ushort maxCapacity)
        {
            DeptCode = deptCode;
            CourseNumber = courseNumber;
            SectionNumber = sectionNumber;
            CreditHours = 3; //Hard coded default credit hours
            MaxCapacity = maxCapacity;
            numOfCurrentlyEnrolled = 0;
        }
        #endregion

        /******** Built in Methods ********/
        #region
        public override string ToString()
        {
            return DeptCode + " " + CourseNumber + "-" + SectionNumber + "  (" + NumOfCurrentlyEnrolled + "/" + MaxCapacity + ")";
        }

        public int CompareTo(Object alpha)
        {
            if (alpha == null) return 1;
            Course c = alpha as Course;
            int sortLevel1 = this.deptCode.ToString().CompareTo(c.deptCode.ToString());
            if (sortLevel1 != 0) return sortLevel1;
            else return this.courseNumber.ToString().CompareTo(c.courseNumber.ToString());
        }
        #endregion
    }
}
