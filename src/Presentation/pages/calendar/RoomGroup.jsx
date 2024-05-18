import React from "react";
import { Room } from "./Room";
import { Availability } from "./Availability";

export function RoomGroup({ roomGroup }) {

  return (
    <div className="w-full" key={roomGroup.name}>
      <div className="flex w-full">
        <div className="flex shrink-0 items-center w-60 pl-2 h-15 font-bold border-b border-gray-200">{roomGroup.name}</div>
        <div className="flex">
          {roomGroup.availabilitys.map(availability => <Availability availability={availability} key={availability.date} />)}
        </div>
      </div>
      <div>
        {roomGroup.rooms.map(room => <Room room={room} key={room.code} />)}
      </div>
    </div>
  );
}
