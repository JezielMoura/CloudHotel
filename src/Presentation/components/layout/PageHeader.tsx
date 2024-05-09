import React, { ReactElement } from "react";

export function PageHeader({ title, children }: {title:string, children?: ReactElement}) {
  return (
    <div className="flex items-center justify-between h-16 border-b border-gray-200">
      <p className="px-6 text-xl">{title}</p>
      { children }
    </div>
  )
}