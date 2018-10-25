using AutoMapper;
using IAttend.API.Dtos;
using IAttend.API.Models;

namespace IAttend.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<StudentSubject,StudentSubjectDto>();
            CreateMap<Instructor,InstructorDto>();
            CreateMap<Schedule,SubjectDto>()
            .ForMember(dest => dest.Name, opt => {
                opt.MapFrom(src => src.Subject.Name);
            })
            .ForMember(dest => dest.Time,opt => {
                opt.ResolveUsing(d => d.Time.ToShortTimeString());
            })
            .ForMember(dest => dest.DayOfWeek,opt => {
                opt.ResolveUsing(d => d.DayOfWeek.ToDayInWord());
            });
        }
    }
}