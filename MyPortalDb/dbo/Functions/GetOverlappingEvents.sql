CREATE FUNCTION GetOverlappingEvents(
    @StartTime datetime,
    @EndTime datetime,
    @EventTypeFilter uniqueidentifier = null
    )
    RETURNS TABLE
    AS
    RETURN SELECT DE.Id,
                  DE.EventTypeId,
                  DE.CreatedById,
                  DE.CreatedDate,
                  DE.RoomId,
                  DE.Subject,
                  DE.Description,
                  DE.Location,
                  DE.StartTime,
                  DE.EndTime,
                  DE.AllDay,
                  DE.[Public],
               DE.System
           FROM dbo.DiaryEvents DE
           WHERE (DE.EndTime >= @StartTime AND ((DE.AllDay = 1 AND DE.StartTime < DATEADD(DAY, 1, @EndTime)) OR DE.StartTime <= @EndTime))
             AND (@EventTypeFilter IS NULL OR DE.EventTypeId = @EventTypeFilter);