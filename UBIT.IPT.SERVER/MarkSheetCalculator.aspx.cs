using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBIT.IPT.SERVER.ModelClasses;

namespace UBIT.IPT.SERVER
{
    public partial class MarkSheetCalculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet =true)]

        public static string GetMarksSheet()
        {
            string request = HttpContext.Current.Request.Params["request"];

            MarksInfo[] marksInfo = JsonConvert.DeserializeObject<MarksInfo[]>(request);

            int totalSubjects = 0;
            int marksSecured = 0;

            int minMarks = marksInfo[0].subjectMarksObtained;
            string minMarksSubject = marksInfo[0].subjectName; 
            int maxMarks = marksInfo[0].subjectMarksObtained;
            string maxMarksSubject = marksInfo[0].subjectName;

            for (int i = 0; i < marksInfo.Length; i++)
            {
                MarksInfo currentInfo = marksInfo[i];
                marksSecured += currentInfo.subjectMarksObtained;
                totalSubjects += 1;

                if(currentInfo.subjectMarksObtained < minMarks)
                {
                    minMarks = currentInfo.subjectMarksObtained;
                    minMarksSubject = currentInfo.subjectName;
                }

                if(currentInfo.subjectMarksObtained > maxMarks)
                {
                    maxMarks = currentInfo.subjectMarksObtained;
                    maxMarksSubject = currentInfo.subjectName;
                }

            }

            float average = marksSecured / totalSubjects;

            MarksInfoResult result = new MarksInfoResult();
            result.minMarks = minMarks;
            result.minMarksSubject = minMarksSubject;
            result.maxMarks = maxMarks;
            result.maxMarksSubject = maxMarksSubject;
            result.average = average;

            string resultStr = JsonConvert.SerializeObject(result);
            return resultStr;

        }


    }
}