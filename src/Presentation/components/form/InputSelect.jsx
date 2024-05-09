import React from "react";

export function InputSelect({ name, value = "", label, children }) {
  return (
    <div className="w-full">
      <label htmlFor={name} className="block py-0.5">{label}</label>
      <select 
        className="border-gray-200 px-2 py-1 rounded text-gray-500 w-full mb-2" 
        name={name} 
        id={name}
        defaultValue={value}>
        {children}
      </select>
    </div>
  )
}