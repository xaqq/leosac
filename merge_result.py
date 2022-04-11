
import os
import sys
import xml.etree.ElementTree as ET
from dataclasses import dataclass, field

def main():
    args = sys.argv[1:]
    merge_results(args[:])

@dataclass
class Suite:
    name: str = "UNAMED"
    timestamp: str = ""
    failures: int = 0
    disabled: int = 0
    skipped: int  = 0
    errors: int   = 0
    tests: int    = 0
    time: float   = 0
    
    # list of xml element
    cases: [] = field(default_factory=list)
    
def get_suite_by_name(suites, name):
    if name not in suites:
        suites[name] = Suite(name=name)
    return suites[name]
    
def get_all_testsuite(xml_file):
        tree = ET.parse(xml_file)
        test_suites = tree.getroot()
        assert(test_suites.tag == "testsuites")

        children = []
        for child in test_suites:
            children.append(child)
        return children

def merge_results(xml_files):
    xml_suites = []
    for file_name in xml_files:
        xml_suites.extend(get_all_testsuite(file_name))

    suites = {}
    for test_suite in xml_suites:
        # Merge suite stats
        s = get_suite_by_name(suites, test_suite.attrib["name"])
        s.timestamp = test_suite.attrib["timestamp"]
        s.tests += int(test_suite.attrib["tests"])
        s.failures += int(test_suite.attrib["failures"])
        s.disabled += int(test_suite.attrib["disabled"])
        s.skipped += int(test_suite.attrib["skipped"])
        s.errors += int(test_suite.attrib["errors"])
        s.time += float(test_suite.attrib["time"])
        for test_case in test_suite:
            # Just add xml to proper Suite
            s.cases.append(test_case)

    total = Suite()
    new_xml_suites = []
    for test_suite in suites.values():
        # Update total stats
        total.timestamp = test_suite.timestamp
        total.tests += test_suite.tests
        total.failures += test_suite.failures
        total.disabled += test_suite.disabled
        total.errors += test_suite.errors
        total.time += test_suite.time

        # Create new "merged" by name suite.
        new_xml_suite = ET.Element('testsuite')
        new_xml_suite.attrib['timestamp'] = str(test_suite.timestamp)
        new_xml_suite.attrib['tests'] = str(test_suite.tests)
        new_xml_suite.attrib['failures'] = str(test_suite.failures)
        new_xml_suite.attrib['disabled'] = str(test_suite.disabled)
        new_xml_suite.attrib['skipped'] = str(test_suite.skipped)
        new_xml_suite.attrib['errors'] = str(test_suite.errors)
        new_xml_suite.attrib['time'] = str(test_suite.time)

        new_xml_suite.extend(test_suite.cases)
        new_xml_suites.append(new_xml_suite)
        
    new_root = ET.Element('testsuites')
    new_root.attrib["name"] = "All Tests"
    new_root.attrib['timestamp'] = str(total.timestamp)
    new_root.attrib['tests'] = str(total.tests)
    new_root.attrib['failures'] = str(total.failures)
    new_root.attrib['disabled'] = str(total.disabled)
    new_root.attrib['errors'] = str(total.errors)
    new_root.attrib['time'] = str(total.time)
    for x in new_xml_suites:
        new_root.append(x)
    new_tree = ET.ElementTree(new_root)
    ET.dump(new_tree)

if __name__ == '__main__':
    main()
