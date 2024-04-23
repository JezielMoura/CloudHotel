import React from "react";

export function Button({ onClick, children}) {
  return (
    <button className="bg-violet-800 text-white px-4 py-1 rounded border border-violet-800 cursor-pointer" onClick={onClick}>
      {children}
    </button>
  )
}