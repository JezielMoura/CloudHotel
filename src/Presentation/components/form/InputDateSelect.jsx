import React from "react";

export function InputSelect({ name, children }) {
  return (
    <select className="border-gray-200 px-2 py-1 rounded text-gray-500 w-full mt-3" name={name} id={name}>
      {children}
    </select>
  )
}