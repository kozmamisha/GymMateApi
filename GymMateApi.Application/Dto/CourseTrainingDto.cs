﻿namespace GymMateApi.Application.Dto;

public class CourseTrainingDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}