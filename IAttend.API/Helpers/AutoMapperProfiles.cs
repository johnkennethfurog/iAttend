using AutoMapper;
using IAttend.API.Dtos;
using IAttend.API.Models;

namespace IAttend.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Pocos.StudentSubject,StudentSubjectDto>()
            .ForMember(dest => dest.Time,opt => {
                opt.ResolveUsing(d => d.Time.ToShortTimeString());
            })
            .ForMember(dest => dest.DayOfWeek,opt => {
                opt.ResolveUsing(d => d.DayOfWeek.ToDayInWord());
            });

            // CreateMap<Instructor,TeacherDto>();
            // CreateMap<Schedule,SubjectDto>()
            // .ForMember(dest => dest.Name, opt => {
            //     opt.MapFrom(src => src.Subject.Name);
            // })
            // .ForMember(dest => dest.Time,opt => {
            //     opt.ResolveUsing(d => d.Time.ToShortTimeString());
            // })
            // .ForMember(dest => dest.DayOfWeek,opt => {
            //     opt.ResolveUsing(d => d.DayOfWeek.ToDayInWord());
            // });

            CreateMap<Pocos.TeacherSubject, TeacherSubjectDto>()
            .ForMember(dest => dest.Name, opt => {
                opt.MapFrom(src => src.Subject);
            })
            .ForMember(dest => dest.Code,opt =>{
                opt.MapFrom(src => src.SubjectCode);
            })
            .ForMember(dest => dest.SchedID, opt => {
                opt.MapFrom(src => src.ID);
            })
            .ForMember(dest => dest.Time,opt => {
                opt.ResolveUsing(d => d.Time.ToShortTimeString());
            })
            .ForMember(dest => dest.DayOfWeek,opt => {
                opt.ResolveUsing(d => d.DayOfWeek.ToDayInWord());
            })
            .ForMember(dest => dest.StudentCount,opt => {
                opt.ResolveUsing(d => d.StudCount);
            });

            CreateMap<Attendance,StudentAttendanceDto>()
            .ForMember(dest => dest.Date, opt => {
                opt.MapFrom(src => src.Date);
            })
            .ForMember(dest => dest.IsPresent, opt =>
            {
                opt.MapFrom(d => d.StudentAttendances.Count > 0);
            });

            CreateMap<Pocos.StudentsSubjectAttendance, StudentDto>()
                .ForMember(dest => dest.IsScanned, opt =>
                {
                    opt.MapFrom(src => src.IsScanned);
                })
                .ForMember(dest => dest.IsPresent, opt =>
                {
                    opt.ResolveUsing(src => src.IsScanned != null);
                });

            CreateMap<Attendance,ActiveAttendanceSessionDto>()
            .ForMember(dest => dest.AttendanceSessionId, opt => {
                opt.MapFrom(src => src.ID);
            })
            .ForMember(dest => dest.SchedId, opt => {
                opt.MapFrom(src => src.ScheduleID);
            });
        }
    }
}