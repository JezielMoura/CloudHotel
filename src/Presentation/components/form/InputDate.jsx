import React from "react";

export function InputDate({ name, value = "", label = "", width = "", required = false}) {
  return (
    <div className="w-full">
      {label ? <label htmlFor={name} className="block py-0.5">{label}</label> : null }
      <input 
        type="date" 
        className={`border-gray-200 px-2 pt-0.5 rounded text-gray-500 ${width}`} 
        name={name} 
        id={name} 
        required={required}
        defaultValue={value} />
    </div>
  )
}