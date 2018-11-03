/*****************************************************
 * Assignment: 3                                     *
 * Due Date: Thursday, 11th Oct                      *
 *                                                   *
 * Name:Sai keerthi Tsundupalli (Z1836733)           *
 * Partner Name: Komal Thakkar (Z1834925)            *
 *                                                   *
 ****************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Tsundupalli_Assign3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, int> item in Grades.GradingList)
            {
                Filter2Grade_ComboBox.Items.Add(item.Key);
            }
            foreach (string m in Course.Major)
            {
                Filter3Major_ComboBox.Items.Add(m);
            }
        }


        /******** ShowResult1_Button_Click shows Grade report for One Student ********/
        private void ShowResult1_Button_Click(object sender, EventArgs e)
        {
            string zidinput = ZID_TextBox.Text;
            zidinput = zidinput.Contains('z') ? zidinput.Replace('z', ' ').Trim() : zidinput;

            if (zidinput != null)
            {
                var studentlist = from rec in Grades.GradesPool
                                  where rec.zID.ToString() == zidinput
                                  orderby rec.DeptCode, rec.CourseNumber
                                  select rec;
                Query_Result_RichTextBox.Clear();
                if (studentlist.Count() > 0)
                {
                    Query_Result_RichTextBox.AppendText(String.Format("Single Student Report for ({0,0} )\n", zidinput));
                    Query_Result_RichTextBox.AppendText("-----------------------------------------------------\n\n");
                    foreach (Grades rec in studentlist)
                    {
                        Query_Result_RichTextBox.AppendText(rec.ToString()+"\n");
                    }
                    Query_Result_RichTextBox.AppendText("\n### END RESULTS ###");
                }
            }
        }

        /******** ShowResult2_Button_Click shows Grade Threshold for One Course ********/
        private void ShowResult2_Button_Click(object sender, EventArgs e)
        {
            if (Filter2LessThan_RadioButton.Checked || Filter2GreaterThan_RadioButton.Checked)
            {
                var gradeValue = Filter2Grade_ComboBox.SelectedItem.ToString();

                string[] selectedCourse = Filter2Course_TextBox.Text.Trim().Split(' ');


                var query = from rec in Grades.GradesPool
                            where Grades.GradingList[rec.Grade] >= Grades.GradingList[gradeValue]
                            group rec by new { rec.zID, rec.DeptCode, rec.CourseNumber, rec.Grade } into g
                            select new
                            {
                                g.Key.zID,
                                g.Key.DeptCode,
                                g.Key.CourseNumber,
                                g.Key.Grade
                            };
                if (Filter2GreaterThan_RadioButton.Checked)
                {
                    query = from rec in Grades.GradesPool
                            where Grades.GradingList[rec.Grade] <= Grades.GradingList[gradeValue]
                            group rec by new { rec.zID, rec.DeptCode, rec.CourseNumber, rec.Grade } into g
                            select new { g.Key.zID, g.Key.DeptCode, g.Key.CourseNumber, g.Key.Grade };
                }
                if (selectedCourse.Length == 2)
                {
                    string deptCode = selectedCourse[0];
                    string courseNum = selectedCourse[1];

                    var list = from rec in query
                               where (rec.DeptCode == deptCode) && (rec.CourseNumber.ToString() == courseNum)
                               select rec;

                    Query_Result_RichTextBox.Clear();
                    if (list.Count() > 0)
                    {
                        Query_Result_RichTextBox.AppendText(String.Format("Grade Threshold Report for ({0,0} {1,0})\n", deptCode, courseNum));
                        Query_Result_RichTextBox.AppendText("-----------------------------------------------------\n\n");
                        foreach (var rec in list)
                        {
                            Query_Result_RichTextBox.AppendText(String.Format("z{0,-10} | {1,6}-{2,-6}   |  {3,5}\n", rec.zID, rec.DeptCode, rec.CourseNumber, rec.Grade));
                        }
                        Query_Result_RichTextBox.AppendText("\n### END RESULTS ###");
                    }
                    else
                    {
                        Query_Result_RichTextBox.Text = "No Result Found.";
                    }
                }
            }
        }

        /******** ShowResult3_Button_Click shows Major Students Who Failed A Course ********/
        private void ShowResult3_Button_Click(object sender, EventArgs e)
        {
            string selectedMajor = Filter3Major_ComboBox.SelectedItem.ToString();
            string[] selectedCourse = Filter3Course_TextBox.Text.Trim().Split(' ');

            if (selectedCourse.Length == 2)
            {
                string deptCode = selectedCourse[0];
                string courseNum = selectedCourse[1];

                var failedStudents = (from rec in Grades.GradesPool
                                      where (rec.Grade == "F") && (rec.DeptCode == deptCode) && (rec.CourseNumber.ToString() == courseNum)
                                      group rec by new { rec.zID, rec.DeptCode, rec.CourseNumber } into g
                                      select new { g.Key.zID, g.Key.DeptCode, g.Key.CourseNumber });
                var Majorlist = from rec in failedStudents
                                from item in Student.StudentPool
                                from x in Grades.GradesPool
                                where (rec.zID == item.ZID
                                && x.ZID == rec.zID
                                && rec.DeptCode == x.DeptCode
                                && rec.CourseNumber == x.CourseNumber
                                && x.DeptCode == rec.DeptCode
                                && x.CourseNumber == x.CourseNumber
                                && item.Major == selectedMajor
                                && x.Grade == "F")
                                select new { rec.zID, rec.DeptCode, rec.CourseNumber, item.Major, x.Grade };

                Query_Result_RichTextBox.Clear();

                if (Majorlist.Count() > 0)
                {
                    Query_Result_RichTextBox.AppendText(String.Format("Fail Report for Majors ({0,0}) in {1,0} {2,0} \n", selectedMajor, deptCode, courseNum));
                    Query_Result_RichTextBox.AppendText("-----------------------------------------------------\n\n");
                    foreach (var rec in Majorlist)
                    {
                        Query_Result_RichTextBox.AppendText(String.Format("z{0,-10} | {1,6}-{2,-6}   |  {3,5}\n", rec.zID, rec.DeptCode, rec.CourseNumber, rec.Grade));
                    }
                    Query_Result_RichTextBox.AppendText("\n### END RESULTS ###");
                }
            }
        }

        /******** ShowResult4_Button_Click shows Grade report for One Course ********/
        private void ShowResult4_Button_Click(object sender, EventArgs e)
        {
            string[] selectedCourse = Filter4Course_TextBox.Text.Trim().Split(' ');

            if (selectedCourse.Length == 2)
            {
                string deptCode = selectedCourse[0];
                string courseNum = selectedCourse[1];

                var studentlist = from rec in Grades.GradesPool
                                  where (rec.DeptCode == deptCode) && (rec.CourseNumber.ToString() == courseNum)
                                  select rec;

                Query_Result_RichTextBox.Clear();
                if (studentlist.Count() > 0)
                {
                    Query_Result_RichTextBox.AppendText(String.Format("Grade Report for ({0,0} {1,0})\n", deptCode, courseNum));
                    Query_Result_RichTextBox.AppendText("-----------------------------------------------------\n\n");
                    foreach (Grades rec in studentlist)
                    {
                        Query_Result_RichTextBox.AppendText(rec.ToString());
                    }
                    Query_Result_RichTextBox.AppendText("\n### END RESULTS ###");
                }
                else
                {
                    Query_Result_RichTextBox.Text = "No Result Found.";
                }
            }
        }

        /******** ShowResult5_Button_Click shows Fail Report for All Courses ********/
        private void ShowResult5_Button_Click(object sender, EventArgs e)
        {
            if (Filter5LessThan_RadioButton.Checked || Filter5Greater_RadioButton.Checked)
            {
                var percentageValue = Filter5Per_NumericUpDown.Value;
                var expression = Filter5Greater_RadioButton.Checked ? ">=" : "<=";

                var enrolledStudents = (from rec in Grades.GradesPool
                                        group rec by new { rec.DeptCode, rec.CourseNumber } into g
                                        select new { g.Key.DeptCode, g.Key.CourseNumber, Count = g.Count() });

                var failedStudents = (from rec in Grades.GradesPool
                                      where (rec.Grade == "F")
                                      group rec by new { rec.DeptCode, rec.CourseNumber } into g
                                      select new { g.Key.DeptCode, g.Key.CourseNumber, Count = g.Count() }
                                      );

                var studentsList = (from x in enrolledStudents
                                    from y in failedStudents
                                    where (x.DeptCode == y.DeptCode) &&
                                    (x.CourseNumber == y.CourseNumber) &&
                                    (((float)y.Count / x.Count) * 100) <= (float)percentageValue
                                    orderby x.DeptCode
                                    select new { x.DeptCode, x.CourseNumber, eCount = x.Count, fCount = y.Count, per = ((float)y.Count / x.Count * 100) }
                                    );

                if (Filter5Greater_RadioButton.Checked)
                {
                    studentsList = (from x in enrolledStudents
                                    from y in failedStudents
                                    where (x.DeptCode == y.DeptCode) &&
                                    (x.CourseNumber == y.CourseNumber) &&
                                    (((float)y.Count / x.Count) * 100) >= (float)percentageValue
                                    orderby x.DeptCode
                                    select new { x.DeptCode, x.CourseNumber, eCount = x.Count, fCount = y.Count, per = ((float)y.Count / x.Count * 100) }
                                    );
                }
                Query_Result_RichTextBox.Clear();
                if (studentsList.Count() > 0)
                {
                    Query_Result_RichTextBox.AppendText(String.Format("Fail Percentage ({0,0} {1,0}%)  Report for Classes.\n", expression, percentageValue));
                    Query_Result_RichTextBox.AppendText("----------------------------------------------------------------\n\n");
                    foreach (var rec in studentsList)
                    {
                        Query_Result_RichTextBox.AppendText(String.Format("Out of {0,0} enrolled in {1,0}-{2,0}, {3,3} failed ( {4:0.00}%)\n\n", rec.eCount, rec.DeptCode, rec.CourseNumber, rec.fCount, rec.per));
                    }
                    Query_Result_RichTextBox.AppendText("\n### END RESULTS ###");
                }
                else
                {
                    Query_Result_RichTextBox.Text = "No Result Found.";
                }
            }
            else
            {
                MessageBox.Show("Please select any radio button");
            }
        }

        /******** ShowResult6_Button_Click shows Pass Report All Courses ********/
        private void ShowResult6_Button_Click(object sender, EventArgs e)
        {
            if (Filter6LessThan_RadioButton.Checked || Filter6GreaterThan_RadioButton.Checked)
            {
                var gradeValue = Filter6Grade_ComboBox.SelectedItem.ToString();
                var expression = Filter6GreaterThan_RadioButton.Checked ? ">=" : "<=";

                var enrolledStudents = (from rec in Grades.GradesPool
                                        group rec by new { rec.DeptCode, rec.CourseNumber } into g
                                        select new { g.Key.DeptCode, g.Key.CourseNumber, Count = g.Count() });

                var passedStudents = (from rec in Grades.GradesPool
                                      where rec.Grade != "F"
                                      && (Grades.GradingList[rec.Grade] >= Grades.GradingList[gradeValue])
                                      group rec by new { rec.DeptCode, rec.CourseNumber } into g
                                      select new { g.Key.DeptCode, g.Key.CourseNumber, Count = g.Count() }
                                      );

                if (Filter6GreaterThan_RadioButton.Checked)
                {
                    passedStudents = (from rec in Grades.GradesPool
                                      where rec.Grade != "F"
                                      && (Grades.GradingList[rec.Grade] <= Grades.GradingList[gradeValue])
                                      group rec by new { rec.DeptCode, rec.CourseNumber } into g
                                      select new { g.Key.DeptCode, g.Key.CourseNumber, Count = g.Count() }
                                      );
                }
                var studentsList = (from x in enrolledStudents
                                    from y in passedStudents
                                    where (x.DeptCode == y.DeptCode)
                                    && (x.CourseNumber == y.CourseNumber)
                                    orderby x.DeptCode, x.CourseNumber
                                    select new { x.DeptCode, x.CourseNumber, eCount = x.Count, fCount = y.Count, per = ((float)y.Count / x.Count * 100) }
                                    );

                Query_Result_RichTextBox.Clear();
                if (studentsList.Count() > 0)
                {
                    Query_Result_RichTextBox.AppendText(String.Format("Pass Percentage ({0,0} {1,0}%)  Report for Classes.\n", expression, gradeValue));
                    Query_Result_RichTextBox.AppendText("----------------------------------------------------------------\n\n");
                    foreach (var rec in studentsList)
                    {
                        Query_Result_RichTextBox.AppendText(String.Format("Out of {0,0} enrolled in {1,0}-{2,0}, {3,3} passed at or below this threshold ( {4:0.00}%)\n\n", rec.eCount, rec.DeptCode, rec.CourseNumber, rec.fCount, rec.per));

                    }
                    Query_Result_RichTextBox.AppendText("\n### END RESULTS ###");
                }
                else
                {
                    Query_Result_RichTextBox.Text = "No Result Found.";
                }
            }
            else
            {
                MessageBox.Show("Please select any radio button");
            }
        }
    }
}