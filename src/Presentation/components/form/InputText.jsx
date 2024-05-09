import React from "react";

export function InputText({ name, value = "", required = false, label= "" }) {
  return (
    <div className="w-full">
      <label 
        htmlFor={name} 
        className="block py-0.5">{label}
      </label>
      <input 
        type="text" 
        className="border-gray-200 px-2 py-0.5 rounded w-full mb-2" 
        name={name} 
        id={name}
        defaultValue={value}
        required={required} />
    </div>
  )
}