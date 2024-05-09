import React from "react";

export function InputNumber({ name, value = 0, required = false, label= "" }) {
  return (
    <div className="w-full">
      <label 
        htmlFor={name} 
        className="block py-0.5">{label}
      </label>
      <input 
        type='number' 
        step="0.01" 
        defaultValue={value?.toFixed(2)} 
        className="border-gray-200 px-2 py-0.5 rounded w-full mb-2" 
        name={name} 
        id={name} 
        required={required} />
    </div>
  )
}