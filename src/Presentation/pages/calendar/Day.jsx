import React from "react";
import { Reservation } from "./Reservation";


export function Day({ day }) {
  return (
    <div className="flex w-20 h-8 p-1 border-l border-b border-gray-200">
      <div className="relative w-full h-full">
        {day.reservation ? <Reservation reservation={day.reservation} /> : null}
      </div>
    </div>
  );
}
