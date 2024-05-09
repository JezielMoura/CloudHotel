import { isoToDay } from '../../helpers/dateFormat';
import React from "react";

export function Availability({ availability }) {

  return (
    <div className="flex flex-col w-20 h-10 p-1 border-l border-b border-gray-200">
      <p className="text-sm block h-4 text-center">{isoToDay(availability.date)}</p>
      <p className="text-sm block h-5 text-center">{availability.value}</p>
    </div>
  );
}
