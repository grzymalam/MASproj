interface TabPanelProps {
  children: React.ReactNode;
  index: number;
  value: number;
}

const TabPanel = ({children, index, value}: TabPanelProps) => {

  return (
    <div role="tabpanel" hidden={index !== value} id={`tab-panel-${index}`} aria-labelledby={`tab-panel-${index}`}>
      {value === index && (
        children
      )}
    </div>
  )
};

export default TabPanel;
