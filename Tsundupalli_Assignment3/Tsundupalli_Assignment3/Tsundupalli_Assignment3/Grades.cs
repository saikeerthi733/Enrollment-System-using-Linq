using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsundupalli_Assign3
{
    class Grades : IComparable
    {
        /******** Class Attributes and Getter Setter Methods ********/

        #region
        public readonly uint zID;
        private string deptCode;
        private Nullable<uint> courseNumber;
        private string grade;
        public static List<string> Major = new List<string>();
        public static List<Grades> GradesPool = new List<Grades>();
        public static readonly Dictionary<string, int> GradingList = new Dictionary<string, int>();
        //public static readonly Dictionary<string, int> GradingList = new Dictionary<string, int>() {
        //                                    { "A+", 1 }, { "A", 2 }, { "A-", 3 },
        //                                    { "B+", 4 }, { "B", 5 }, { "B", 6 },
        //                                    { "C++", 7 }, { "C", 8 }, { "C-", 9 },
        //                                    { "D", 10 }, { "F", 11 }
        //                                };

        public uint ZID
        {
            get { return zID; }
        }
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
        public string Grade
        {
            get { return grade; }
            set
            {
                grade = value;
            }
        }
        #endregion


        /******** Constructor ********/
        #region
        public Grades(uint zid, string deptCode, uint courseNumber, string grade)
        {
            zID = zid;
            DeptCode = deptCode;
            CourseNumber = courseNumber;
            Grade = grade;
        }
        #endregion

        /******** Built in Methods ********/
        #region
        public override string ToString()
        {
            return String.Format("z{0,-10} | {1,6}-{2,-6} |    {3,0}", ZID, DeptCode, CourseNumber, Grade);
        }

        public int CompareTo(Object alpha)
        {
            if (alpha == null) return 1;
            Grades g = alpha as Grades;
            if (g != null)
            {
                return this.ZID.CompareTo(g.ZID);
            }
            else
            {
                throw new ArgumentException("[Student]:CompareTo argument is not a Student");
            }
        }
        #endregion
    }
}
