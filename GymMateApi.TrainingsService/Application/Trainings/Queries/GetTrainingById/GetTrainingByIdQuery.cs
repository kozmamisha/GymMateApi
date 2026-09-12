using GymMateApi.TrainingsService.Application.Dto;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Queries.GetTrainingById;

public record GetTrainingByIdQuery(Guid Id) : IRequest<TrainingDto>;
