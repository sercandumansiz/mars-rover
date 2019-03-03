using MarsRover.Program.Models;
using MarsRover.Program.Common.Exceptions.PlateauExceptions;
using System;
using Microsoft.Extensions.Logging;

namespace MarsRover.Program.Services.Implementations
{
    public class PlateauService : IPlateauService
    {
        private readonly ILogger<PlateauService> _logger;

        public PlateauService(ILogger<PlateauService> logger)
        {
            _logger = logger;
        }

        public BaseModel<PlateauModel> Create(int width, int height)
        {
            BaseModel<PlateauModel> model = new BaseModel<PlateauModel>();

            PlateauModel plateauModel = new PlateauModel()
            {
                Width = width,
                Height = height
            };

            BaseModel<bool> validationResponse = Validate(plateauModel);

            if (validationResponse.Data)
            {
                model.Data = plateauModel;
                _logger.LogInformation("Plateau created successfully.");
            }

            return model;
        }

        public BaseModel<bool> Validate(PlateauModel plateau)
        {
            BaseModel<bool> model = new BaseModel<bool>();

            if (plateau != null && plateau.Width > 0 && plateau.Height > 0)
            {
                model.Data = true;
                _logger.LogInformation($"Plateau has valid size ({plateau.Width}-{plateau.Height}).");
            }
            else
            {
                ValidatePlateauException exception = new ValidatePlateauException();
                _logger.LogError(exception.Message);
                throw exception;
            }

            return model;
        }
    }
}