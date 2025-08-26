using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;

namespace GymMateApi.Application.Services
{
    public class TrainingService(ITrainingRepository trainingRepository) : ITrainingService
    {
        public async Task CreateAsync(string name, string description, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("Training name cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                throw new BadRequestException("Training description cannot be empty");
            
            TrainingEntity training = new()
            {
                Name = name,
                Description = description
            };

            await trainingRepository.CreateTraining(training, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var training = await trainingRepository.GetTrainingById(id, cancellationToken)
                ?? throw new EntityNotFoundException("Training not found");

            await trainingRepository.DeleteTraining(training, cancellationToken);
        }

        public async Task<List<TrainingDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var trainings = await trainingRepository.GetAllTrainings(cancellationToken);

            return trainings.ToDtoList();
        }

        public async Task<TrainingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var training = await trainingRepository.GetTrainingById(id, cancellationToken)
                ?? throw new EntityNotFoundException("Training not found");

            return training.ToDto();
        }

        public async Task UpdateAsync(Guid id, string name, string description, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("Training name cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                throw new BadRequestException("Training description cannot be empty");

            var currentTraining = await trainingRepository.GetTrainingById(id, cancellationToken)
                ?? throw new EntityNotFoundException("Training not found");

            currentTraining.Name = name;
            currentTraining.Description = description;

            await trainingRepository.UpdateTraining(currentTraining, cancellationToken);
        }
    }
}
