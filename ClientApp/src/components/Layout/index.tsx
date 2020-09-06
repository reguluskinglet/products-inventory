import React, { PropsWithChildren } from 'react';

export const PrivateLayout: React.FC<any> = ({ children }: PropsWithChildren<any>) => (
  <div>
    {children}
  </div>
);
