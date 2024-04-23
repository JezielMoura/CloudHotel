import React from "react";
import { Day } from "./Day";


export function Room({ room }) {

  return (
    <div className="w-full flex" key={room.code}>
      <div className="flex shrink-0 h-8 w-60  pl-2 items-center border-b border-gray-200" key={room.code}>{room.code}</div>
      <div className="flex">
        {room.days.map(day => <Day day={day} key={day.date} />)}
      </div>
    </div>
  );
}
