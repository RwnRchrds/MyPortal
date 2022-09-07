-- =============================================
-- Author:		R Richards
-- Create date: 21 Jul 2022
-- Description:	Get events that overlap from the DiaryEvents table
-- =============================================
CREATE FUNCTION [dbo].[GetOverlappingEvents]
(
	-- Add the parameters for the function here
	@StartTime datetime,
	@EndTime datetime,
	@EventTypeId uniqueidentifier
)
RETURNS TABLE
AS
RETURN
(
	-- Add the SELECT statement with parameter references here
    SELECT
        [DE].[Id],
        [DE].[EventTypeId],
        [DE].[RoomId],
        [DE].[Subject],
        [DE].[Description],
        [DE].[Location],
        [DE].[StartTime],
        [DE].[EndTime],
        [DE].[AllDay],
        [DE].[Public]
    FROM DiaryEvents DE
    WHERE ((DE.EndTime >= @StartTime AND DE.StartTime <= @EndTime) OR (@StartTime >= DE.StartTime AND @EndTime <= DE.EndTime AND DE.AllDay = 1)) AND (@EventTypeId IS NULL OR DE.EventTypeId = @EventTypeId)
)