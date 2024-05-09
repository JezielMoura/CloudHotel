import React from "react";

export function Reservation({ reservation }) {
  const statusColors = ['bg-cyan-600', 'bg-sky-500', 'bg-green-500', 'bg-gray-700', 'bg-red-500'];

  return (
    <div className={`absolute z-10 h-6 left-10 rounded ${statusColors[reservation.status]}`} style={{ width: `${80 * reservation.days - 5}px` }}>
      <div className="h-full relative">
        <p className="px-1 h-full overflow-clip text-white text-sm" style={{ lineHeight: '24px' }}>{reservation?.guest}</p>
      </div>
    </div>
  );
}
