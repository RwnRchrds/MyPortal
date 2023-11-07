using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Models.Data.School;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Calendar
{
    public class DiaryEventModel : BaseModelWithLoad, ICloneable
    {
        private DateTime _startTime;
        private DateTime _endTime;

        private DiaryEventModel()
        {
        }

        public DiaryEventModel(DiaryEvent model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(DiaryEvent model)
        {
            EventTypeId = model.EventTypeId;
            CreatedById = model.CreatedById;
            CreatedDate = model.CreatedDate;
            RoomId = model.RoomId;
            Subject = model.Subject;
            Description = model.Description;
            Location = model.Location;
            StartTime = model.StartTime;
            EndTime = model.EndTime;
            AllDay = model.AllDay;
            Public = model.Public;

            if (model.EventType != null)
            {
                EventType = new DiaryEventTypeModel(model.EventType);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.Room != null)
            {
                Room = new RoomModel(model.Room);
            }
        }

        public IEnumerable<DiaryEventModel> CreateSeries(EventFrequency frequency, DateTime endDate)
        {
            var series = new List<DiaryEventModel>();

            series.Add(this);

            DateTime? nextStartTime = StartTime.GetNextOccurrence(frequency);
            TimeSpan duration = EndTime - StartTime;

            while (nextStartTime.HasValue && nextStartTime <= endDate)
            {
                var newEvent = (DiaryEventModel)Clone();
                newEvent.StartTime = nextStartTime.Value;
                newEvent.EndTime = nextStartTime.Value.Add(duration);
                series.Add(newEvent);

                nextStartTime = nextStartTime.Value.GetNextOccurrence(frequency);
            }

            return series;
        }

        public IEnumerable<DiaryEventModel> CreateSeries(WeeklyPatternModel weeklyPattern, DateTime endDate)
        {
            var series = new List<DiaryEventModel>();

            series.Add(this);

            DateTime? nextStartTime = StartTime.GetNextOccurrence(weeklyPattern);
            TimeSpan duration = EndTime - StartTime;

            while (nextStartTime.HasValue && nextStartTime <= endDate)
            {
                var newEvent = (DiaryEventModel)Clone();
                newEvent.StartTime = nextStartTime.Value;
                newEvent.EndTime = nextStartTime.Value.Add(duration);
                series.Add(newEvent);

                nextStartTime = nextStartTime.Value.GetNextOccurrence(weeklyPattern);
            }

            return series;
        }

        public Guid EventTypeId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? RoomId { get; set; }

        [Required] [StringLength(256)] public string Subject { get; set; }

        [StringLength(256)] public string Description { get; set; }

        [StringLength(256)] public string Location { get; set; }

        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public bool AllDay { get; set; }

        public bool Public { get; set; }

        public virtual DiaryEventTypeModel EventType { get; set; }
        public virtual UserModel CreatedBy { get; set; }
        public virtual RoomModel Room { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.DiaryEvents.GetById(Id.Value);

                LoadFromModel(model);
            }
        }

        public object Clone()
        {
            return new DiaryEventModel
            {
                EventTypeId = EventTypeId,
                RoomId = RoomId,
                Subject = Subject,
                Description = Description,
                Location = Location,
                StartTime = StartTime,
                EndTime = EndTime,
                Public = Public
            };
        }
    }
}