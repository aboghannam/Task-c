using Mapster;

namespace Application.Setting.Dtos
{
    public class SettingDto : IRegister
    {
        public List<string> WorkDays { get; set; } = new List<string>();
        public DateTime WorkFrom { get; set; }
        public DateTime WorkTo { get; set; }
        public double PeriodBetweenTimes { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Setting, SettingDto>()
                .Ignore(dest => dest.WorkDays)
                   ; 
        }
    }
}