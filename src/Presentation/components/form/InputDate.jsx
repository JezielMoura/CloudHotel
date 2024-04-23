import React from "react";

export function InputDate({ name}) {
  return (
    <input type="date" className="border-gray-200 px-2 py-1 rounded text-gray-500" name={name} id={name} />
  )
}