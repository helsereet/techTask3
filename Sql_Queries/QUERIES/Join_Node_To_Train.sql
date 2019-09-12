use BusRoutes;

select
	n.Id,
	prev_s.StationName as Previous_Station,
	cur_s.StationName as Current_Station,
	next_s.StationName as Next_Station
FROM
	[NODE] as n
left join [STATION] as prev_s on n.Previous_Station = prev_s.Id
left join [STATION] as cur_s on n.Current_Station = cur_s.Id
left join [STATION] as next_s on n.Next_Station = next_s.Id;