import { Navigate, Route, Routes } from 'react-router-dom';
import { ReactElement } from 'react';
import Dashboard from './Dashboard';
import { Test } from './pages/test';
import { Test2 } from './pages/test2';
import { Start } from './Start';
import { CreateItem } from './applications/CreateItem';
import { ListApplications } from './applications/ListApplications';
import CreateMortgage from './mortgage/CreateMortgage';
import { ListMortgages } from './ListMortgages';


export const Router = (): ReactElement => {
    return (
        <Routes>
            <Route path="/" element={<Dashboard />}>
                <Route path="" element={<Navigate to="start" replace />} />
                <Route path="start" element={<Start />} />
            </Route>
            <Route path="/application" element={<Dashboard />}>
                <Route path="" element={<Navigate to="create" replace />} />
                <Route path="create" element={<CreateItem />} />
                <Route path="list" element={<ListApplications />} />

            </Route>
            <Route path="/mortgages" element={<Dashboard />}>
                <Route path="" element={<Navigate to="create" replace />} />
                <Route path="create" element={<CreateMortgage />} />
                <Route path="list" element={<ListMortgages />} />

            </Route>
            {/* <Route path="/" element={<LogoOnlyLayout />}>
  
                <Route path="" element={<Navigate to="/dashboard" />} />
                <Route path="*" element={<Navigate to="/404" />} />
            </Route>*/}
        </Routes>
    );
};

export default Router;
