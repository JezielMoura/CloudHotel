import React from "react";

export function InputText({ name, label="" }) {
  return (
    <input type="text" className="border-gray-200 px-2 py-1 rounded w-full mt-3" name={name} id={name} placeholder={label} />
  )
}