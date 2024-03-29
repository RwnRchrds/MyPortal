﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITrainingCertificateRepository : IReadWriteRepository<TrainingCertificate>,
        IUpdateRepository<TrainingCertificate>
    {
    }
}