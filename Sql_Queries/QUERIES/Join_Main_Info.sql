use BusRoutes;

select
	p.Train_Id, t.Train_Name, p.Arrival_WeekDay, p.Departure_WeekDay,n.Previous_Station, n.Current_Station, n.Next_Station, r.Route_Name, p.Route_Id
from
	[PATH] as p
inner join(select
				n.Id,
				prev_s.StationName as Previous_Station,
				cur_s.StationName as Current_Station,
				next_s.StationName as Next_Station
			FROM
				[NODE] as n
			left join [STATION] as prev_s on n.Previous_Station = prev_s.Id
			left join [STATION] as cur_s on n.Current_Station = cur_s.Id
			left join [STATION] as next_s on n.Next_Station = next_s.Id) as n on n.Id = p.Node_Id
inner join [TRAIN] as t on t.Id = p.Train_Id
inner join [ROUTE] as r on r.Id = p.Route_Id
order by t.Train_Name, Route_Id asc;