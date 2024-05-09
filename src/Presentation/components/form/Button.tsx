import React from "react";

type Props = {
  onClick?(): void,
  children: any
}

export function Button({ onClick, children}: Props) {
  return (
    <button className="bg-violet-800 text-white px-4 py-1 rounded border border-violet-800 cursor-pointer" onClick={onClick}>
      {children}
    </button>
  )
}