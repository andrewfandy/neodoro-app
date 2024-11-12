using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.TimerSessions;

public record TimerSessionDto(
    int Id,
    
    [Required(ErrorMessage = "ScheduleId is required")]
    int ScheduleId,
    
    [Required(ErrorMessage = "Focus duration is required")]
    TimeSpan FocusDuration,

    [Required(ErrorMessage = "Rest duration is required")]
    TimeSpan RestDuration,
    
    TimeSpan? TotalDuration
    );