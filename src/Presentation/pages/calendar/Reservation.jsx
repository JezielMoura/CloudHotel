import React from "react";


export function Reservation({ reservation }) {
  return (
    <div className="absolute z-10 h-6 bg-sky-500 left-10 rounded" style={{ width: `${80 * reservation.days - 5}px` }}>
      <div className="h-full relative">
        <p className="px-1 h-full overflow-clip text-white text-sm" style={{ lineHeight: '24px' }}>{reservation?.guest}</p>
      </div>
    </div>
  );
}
