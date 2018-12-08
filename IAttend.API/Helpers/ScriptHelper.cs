using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Helpers
{
    public static class ScriptHelper
    {
        public static string StudentsAttendances(int subjectId,DateTime from , DateTime to)
        {
            string sb = "	DECLARE @DATEFROM VARCHAR(50) = '"+from.ToString("MM/dd/yyyy")+"'" +
                        "	DECLARE @DATETO VARCHAR(50) = '"+to.ToString("MM/dd/yyyy") + "'" +
                        "	" +
                        "	DECLARE @TABLE_CREATION VARCHAR(MAX) " +
                        "	SET @TABLE_CREATION =  'DECLARE @TEMP_TABLE TABLE" +
                        "							(" +
                        "								StudentNumber varchar(50)," +
                        "								StudentName varchar(MAX)," +
                        "								IsPresent int," +
                        "								AttendanceDate Date" +
                        "							)" +
                        "							DECLARE @DATE Date" +
                        "							DECLARE @getid CURSOR" +
                        "							SET @getid = CURSOR FOR" +
                        "							SELECT ATT.Date FROM Attendances ATT WHERE ScheduleID = "+ subjectId + " AND Att.Date BETWEEN '''+@DATEFROM+''' AND '''+@DATETO+'''" +
                        "	" +
                        "							OPEN @getid" +
                        "							FETCH NEXT" +
                        "							FROM  @getid INTO @DATE" +
                        "							WHILE  @@FETCH_STATUS = 0" +
                        "							BEGIN" +
                        "	" +
                        "								INSERT INTO @TEMP_TABLE(StudentNumber,StudentName,IsPresent,AttendanceDate)" +
                        "								SELECT STUD_SUBJ.StudentNumber,STUD.StudentName,(CASE WHEN SA.IsScanned IS NULL THEN 0 ELSE 1 END ) AS ''IsPresent'',@DATE FROM (SELECT * FROM StudentSubjects WHERE ScheduleID = "+ subjectId + ") STUD_SUBJ" +
                        "									LEFT JOIN STUDENTS STUD ON STUD.StudentNumber = STUD_SUBJ.StudentNumber" +
                        "									LEFT JOIN (" +
                        "												SELECT SA.IsScanned,SA.ScheduleID,SA.StudentNumber,AT.Date FROM StudentAttendances SA" +
                        "												LEFT JOIN Attendances AT ON AT.ID = SA.AttendanceID" +
                        "												WHERE CAST(AT.Date AS DATE) = @DATE AND SA.ScheduleID = "+ subjectId + ") SA ON SA.StudentNumber = STUD.StudentNumber" +
                        "	" +
                        "								FETCH NEXT" +
                        "								FROM  @getid INTO @DATE" +
                        "							END" +
                        "							CLOSE @getid" +
                        "							DEALLOCATE @getid" +
                        "							" +
                        "							'" +
                        "	DECLARE @PIVOT_DATE VARCHAR(max)" +
                        "	SET @PIVOT_DATE = stuff((" +
                        "	select ',[' + CAST(CAST( ATT.Date AS DATE) as varchar(50)) + ']' from Attendances ATT where ScheduleID = 2 AND ATT.Date BETWEEN @DATEFROM AND @DATETO FOR XML PATH('') " +
                        "    ), 1, 1, '')" +
                        "	" +
                        "	DECLARE @PIVOT_SQL NVARCHAR(MAX)" +
                        "	SET @PIVOT_SQL = 	@TABLE_CREATION + " +
                        "						'SELECT * FROM" +
                        "						(" +
                        "							SELECT * from @TEMP_TABLE" +
                        "							PIVOT" +
                        "							(" +
                        "								AVG(IsPresent)" +
                        "								FOR AttendanceDate IN('+@PIVOT_DATE+')" +
                        "							)" +
                        "						 as PIVOT_TABLE) TBL'" +
                        "	EXECUTE sp_executesql @PIVOT_SQL";

            return sb;
        }
    }
}
