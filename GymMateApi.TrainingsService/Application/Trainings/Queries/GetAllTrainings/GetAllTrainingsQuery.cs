using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Trainings.Queries.GetAllTrainings;

public record GetAllTrainingsQuery() : IRequest<List<TrainingDto>>;