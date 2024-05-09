import React from "react";

export function Loading() {
  return (
    <div className="fixed z-50 top-0 left-0 w-screen h-screen bg-black/10 flex justify-center items-center">
      <div className="relative w-40 h-40">
        <svg className="circular-loader"viewBox="25 25 50 50" >
          <circle className="loader-path" cx="50" cy="50" r="20" fill="none" stroke="#5B21B6" strokeWidth="3" />
        </svg>
      </div>
    </div>
  )
}