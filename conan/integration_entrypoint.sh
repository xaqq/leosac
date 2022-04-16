#!/bin/bash

prep_term()                               
{                                         
    unset term_child_pid                  
    unset term_kill_needed                
    trap 'handle_term' TERM INT           
}                                         
handle_term()                             
{                                         
    echo "Got term. Child pid= ${term_child_pid}" 
    if [ "${term_child_pid}" ]; then      
        kill -TERM "${term_child_pid}" 2>/dev/null 
    else                                           
        term_kill_needed="yes"                     
    fi                                             
}                                                  
wait_term()                                        
{                                                  
    term_child_pid=$!                             
    if [ "${term_kill_needed}" ]; then             
        kill -TERM "${term_child_pid}" 2>/dev/null 
    fi                                             
    wait ${term_child_pid} 2>/dev/null             
    trap - TERM INT                                
    wait ${term_child_pid} 2>/dev/null             
}                                                 

pushd /leosac_repository/build 
prep_term                
./bin/leosac "$@"  & 
wait_term               
popd 
lcov --no-external --exclude '*_odb_*' --directory . --capture -o /coverage_result/coverage.info
