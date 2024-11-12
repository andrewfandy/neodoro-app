using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Schedules;

public record ScheduleDetailDto(
    
    int Id,
    
    [Required(ErrorMessage = "UserId is required")]
    int UserId,

    [Required(ErrorMessage = "TimerSessionId is required")]
    int TimerSessionId,

    [Required(ErrorMessage = "Schedule name is required")]
    string Name,
    
    [Required(ErrorMessage = "Complete status is required")]
    bool IsComplete,
    
    [Required(ErrorMessage = "Active status is required")]
    bool IsActive,
    
    [Required(ErrorMessage = "Pause status is required")]
    bool IsPause,
    
    DateTime LastModified
    
    );